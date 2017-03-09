import { Component, OnInit } from '@angular/core';
import { CartService } from '../../../services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sub-menu',
  templateUrl: './sub-menu.component.html'
})

export class SubMenuComponent implements OnInit {
  public totalItems: number = 0;
  public displayCart : boolean = false;
  public cartItems: any[];

  constructor(private cartService: CartService, private router: Router) {
    this.cartService.cartChange.subscribe((data) => {
      this.totalItems = data.length;
      this.cartItems = data;
    });

    this.cartService.load();
  }

  ngOnInit() {
  }

  toogleCart(){
    this.displayCart = !this.displayCart;
  }

  getSubTotal(){
    return this.cartService.getSubTotal();
  }

  hasItemsInCart(){
    return this.totalItems > 0;
  }

  removeItemFromCart(item){
    this.cartService.removeItem(item.id);
  }

  checkout(){
    alert('Not implemented!');
  }
}