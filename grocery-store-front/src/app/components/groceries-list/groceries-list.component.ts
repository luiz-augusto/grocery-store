import { Component, OnInit, Input } from '@angular/core';
import { DataService } from '../../services/data.service';
import { CartService } from '../../services/cart.service';
import { Groceries } from '../../models/groceries';
import { FormsModule } from "@angular/forms";
import 'rxjs/add/operator/catch';
import { DragulaModule, DragulaService } from "ng2-dragula";

@Component({
  selector: 'app-groceries-list',
  templateUrl: './groceries-list.component.html',
  providers: [DataService]
})
export class GroceriesListComponent implements OnInit {
  public groceriesList: any[];
  public unities: any[];
  public categories: any[];
  public groceries: any;
  public displayModal: boolean;
  public errors: any[] = [];

  constructor(private dataService: DataService,
    private cartService: CartService,
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
            
          }, err => { this.handlerError(err) });
        }
      });
    }

  ngOnInit() {
    this.displayModal = false;
    this.getGroceriesList();
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

  getGroceriesList() {
    this.dataService.getGroceriesList().subscribe(result => {
      this.groceriesList = result.data.groceriesList;
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

  handlerError(err: any) {
      var ex = JSON.parse(err._body);
      this.errors = ex.errors;
      console.log(err);
  }

  save(groceries) {
      if (this.isInserting(groceries)) {
          this.dataService.insert(groceries).subscribe(result => {
              this.displayModal = false;
              this.getGroceriesList();
          }, err => { this.handlerError(err) });
    }
    else {
      this.dataService.update(groceries).subscribe(result => {
        this.displayModal = false;
        this.getGroceriesList();
      }, err => { this.handlerError(err) });
    }
  }

  delete(groceries) {
    if (confirm("Are you sure that you want delete the item " + groceries.name + "?")) {
      this.dataService.delete(groceries.id).subscribe(result => {
        this.displayModal = false;
        this.getGroceriesList();
      }, err => { this.handlerError(err) });
    }
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
}