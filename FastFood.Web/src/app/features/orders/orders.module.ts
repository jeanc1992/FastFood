import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './pages/orders/orders.component';
import { OrderCardComponent } from './components/order-card/order-card.component';
import { AddOrderComponent } from './components/add-order/add-order.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';
@NgModule({
  declarations: [
    OrdersComponent,
    OrderCardComponent,
    AddOrderComponent,
    
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    TabsModule.forRoot()
  ]
})
export class OrdersModule { }
