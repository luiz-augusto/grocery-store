import { Component, OnInit, Input } from '@angular/core';
import { OrderBy } from "../../utils/OrderBy";
import { DataService } from '../../services/data.service';
import { CartService } from '../../services/cart.service';
import { Groceries } from '../../models/groceries';
import { PagerService } from '../../services/pager.service';
import { FormsModule } from "@angular/forms";
//import { DragulaModule, DragulaService } from "../../../../node_modules/ng2-dragula/ng2-dragula";
import { DragulaModule, DragulaService } from "ng2-dragula"

@Component({
  selector: 'app-groceries-list',
  templateUrl: './groceries-list.component.html',
  providers: [DataService, PagerService]
})
export class GroceriesListComponent implements OnInit {
  public groceriesList: any[];
  public unities: any[];
  public categories: any[];
  public groceries: any;
  public displayModal: boolean;
  @Input() columns: Columns[];
  @Input() sort: Sorting;
  // array of all items to be paged
  private allItems: any[];
  // pager object
  pager: any = {};
  // paged items
  //pagedItems: any[];
  term: string = "";
  public page: number = 1;
  public pageSize: number = 10;
  public dragulaModel : any[];

  constructor(private dataService: DataService,
    private cartService: CartService,
    private pagerService: PagerService,
    private dragulaService: DragulaService) {
      dragulaService.setOptions('fourth-bag', {
      removeOnSpill: false
     });

      dragulaService.drag.subscribe((value) => {
        this.onDrag(value.slice(1));
      });
      dragulaService.drop.subscribe((value) => {
        this.onDrop(value.slice(1));
      });
      dragulaService.over.subscribe((value) => {
        this.onOver(value.slice(1));
      });
      dragulaService.out.subscribe((value) => {
        this.onOut(value.slice(1));
      });
      dragulaService.dragend.subscribe((value) => {
        var el = (value.slice(1))[0];
        if (el){
          var id = el.getAttribute('data-id');
          var index = Array.from(el.parentNode.children).indexOf(el);
          dataService.updatePosition(id, index).subscribe(result => {
            console.log(result);
          });
        }
      });
      // dragulaService.dropModel.subscribe((value) => {
      //   this.onDropModel(value.slice(1));
      // });
    }

  ngOnInit() {
    this.initializeFilters();
    this.displayModal = false;
    //this.getGroceriesList();
    this.setPage(1);
    this.dataService.getUnitiesList().subscribe(result => {
      this.unities = result;
    })
    this.dataService.getCategoriesList().subscribe(result => {
      this.categories = result;
    })
    this.groceries = this.createItem();
  }

  createItem() {
    return new Groceries();
  }

  initializeFilters() {
    this.columns = [
      {
        display: 'Name', //The text to display
        variable: 'name', //The name of the key that's apart of the data array
        filter: 'text' //The type data type of the column (number, text, date, etc.)
      },
      {
        display: 'Price', //The text to display
        variable: 'price', //The name of the key that's apart of the data array
        filter: 'number : 1.2-2' //The type data type of the column (number, text, date, etc.)
      },
      {
        display: 'Unity', //The text to display
        variable: 'unity', //The name of the key that's apart of the data array
        filter: 'text' //The type data type of the column (number, text, date, etc.)
      },
      {
        display: 'Category', //The text to display
        variable: 'category', //The name of the key that's apart of the data array
        filter: 'text' //The type data type of the column (number, text, date, etc.)
      }
    ];

    this.sort = new Sorting('name', false);
  }

  selectedClass(columnName): string {
    if (columnName == this.sort.column)
      return !this.sort.descending ? 'asc' : 'dsc';
    else
      return '';
  }

  changeSorting(columnName): void {
    var sort = this.sort;
    if (sort.column == columnName) {
      sort.descending = !sort.descending;
    } else {
      sort.column = columnName;
      sort.descending = false;
    }
  }

  convertSorting(): any[] {
    var convert = this.sort.descending ? '-' + this.sort.column : this.sort.column;
    return [convert];
  }

  getGroceriesList(term: string, page: number, pageSize: number) {
    this.dataService.getGroceriesList(term, page, pageSize).subscribe(result => {
      this.groceriesList = result.data.groceriesList;
      // get pager object from service
      this.pager = this.pagerService.getPager(result.data.totalItems, page, pageSize);

      console.log(this.pager);
    })
  }

  addToCart(groceries) {
    if (!this.cartService.hasItem(groceries.id))
      this.cartService.addItem({
        id: groceries.id, name: groceries.name, price: groceries.price,
        unity: groceries.unity, category: groceries.category
      });
    else
      this.cartService.removeItem(groceries.id);
  }

  isGloceriesSelected(groceries) {
    return this.cartService.hasItem(groceries.id);
  }

  add() {
    this.groceries = this.createItem();
    this.displayModal = true;
  }

  getItem(id) {
    this.dataService.getItem(id).subscribe(result => {
      this.groceries = result.data;
      this.displayModal = true;
    })
  }

  cancel() {
    this.displayModal = false;
  }

  isInserting(groceries) {
    return groceries.id == "";
  }

  save(groceries) {
    if (this.isInserting(groceries)) {
      this.dataService.insert(groceries).subscribe(result => {
        this.displayModal = false;
        this.getGroceriesList(this.term, this.page, this.pageSize);
      })
    }
    else {
      this.dataService.update(groceries).subscribe(result => {
        this.displayModal = false;
        this.getGroceriesList(this.term, this.page, this.pageSize);
      })
    }
  }

  delete(groceries) {
    if (confirm("Are you sure that you want delete the item " + groceries.name + "?")) {
      this.dataService.delete(groceries.id).subscribe(result => {
        this.displayModal = false;
        this.getGroceriesList(this.term, this.page, this.pageSize);
      })
    }
  }

  setPage(page: number) {
    if (page < 1 || page > this.pager.totalPages) {
      return;
    }

    // get current page of items
    this.getGroceriesList(this.term, page, this.pageSize)
  }

  search(t){
    this.term = t;
    this.setPage(1);
  }

  private hasClass(el: any, name: string) {
    return new RegExp('(?:^|\\s+)' + name + '(?:\\s+|$)').test(el.className);
  }

  private addClass(el: any, name: string) {
    if (!this.hasClass(el, name)) {
      el.className = el.className ? [el.className, name].join(' ') : name;
    }
  }

  private removeClass(el: any, name: string) {
    if (this.hasClass(el, name)) {
      el.className = el.className.replace(new RegExp('(?:^|\\s+)' + name + '(?:\\s+|$)', 'g'), '');
    }
  }

  private onDrag(args) {
    let [e, el] = args;
    this.removeClass(e, 'ex-moved');
  }

  private onDrop(args) {
    let [e, el] = args;
    this.addClass(e, 'ex-moved');
  }

  private onOver(args) {
    let [e, el, container] = args;
    this.addClass(el, 'ex-over');
  }

  private onOut(args) {
    let [e, el, container] = args;
    this.removeClass(el, 'ex-over');
  }

  private onDropModel(args) {
    // let [el, target, source] = args;
    // console.log(args);
    // var index = el.parent.children.indexOf(el);
    // console.log(index);
    // do something else
  }
}

class Sorting {
  column: string; //to match the variable of one of the columns
  descending: boolean;
  constructor(column: string, descending: boolean) {
    this.column = column;
    this.descending = this.descending;
  }
}

class Columns {
  display: string; //The text to display
  variable: string; //The name of the key that's apart of the data array
  filter: string; //The type data type of the column (number, text, date, etc.)
  constructor(display: string, variable: string, filter: string) {
    this.display = display;
    this.variable = variable;
    this.filter = filter;
  }
}