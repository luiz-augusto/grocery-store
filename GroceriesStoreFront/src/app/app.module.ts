import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

// Routes
import { Routing, RoutingProviders } from './app.routing';

// Root
import { AppComponent } from './app.component';

// Shared
import { HeadbarComponent } from './components/shared/headbar/headbar.component';
import { SubMenuComponent } from './components/shared/sub-menu/sub-menu.component';
import { FooterComponent } from './components/shared/footer/footer.component';

// Components
import { GroceriesListComponent } from './components/groceries-list/groceries-list.component';

// Pages
import { HomePageComponent } from './pages/home-page/home-page.component';
//import { LoginPageComponent } from './pages/login-page/login-page.component';
import { SignupPageComponent } from './pages/signup-page/signup-page.component';
import { CartPageComponent } from './pages/cart-page/cart-page.component';

// Services
import { CartService } from './services/cart.service';
import { PagerService } from './services/pager.service';
//import { AuthService } from './services/auth.service';

// Directives
import { NumberDirective } from './directives/number.directive';

import { DragulaModule, DragulaService } from '../../node_modules/ng2-dragula/ng2-dragula';

import { OrderBy } from './utils/OrderBy';

@NgModule({
  declarations: [
    NumberDirective,
    AppComponent,
    HeadbarComponent,
    SubMenuComponent,
    GroceriesListComponent,
    FooterComponent,
    HomePageComponent,
    SignupPageComponent,
    CartPageComponent,
    OrderBy
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    Routing,
    DragulaModule,
  ],
  providers: [CartService, PagerService],
  bootstrap: [AppComponent]
})
export class AppModule { }
