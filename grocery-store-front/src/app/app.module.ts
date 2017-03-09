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

// Components
import { GroceriesListComponent } from './components/groceries-list/groceries-list.component';

// Pages
import { HomePageComponent } from './pages/home-page/home-page.component';

// Services
import { CartService } from './services/cart.service';

import { DragulaModule, DragulaService } from '../../node_modules/ng2-dragula/ng2-dragula';

@NgModule({
  declarations: [
    AppComponent,
    HeadbarComponent,
    SubMenuComponent,
    GroceriesListComponent,
    HomePageComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    Routing,
    DragulaModule,
  ],
  providers: [CartService],
  bootstrap: [AppComponent]
})
export class AppModule { }
