import { Component, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { OrderStatusType } from 'src/app/core/enums/orderStatusType';
import { OrderResponseDto } from 'src/app/core/models/orders/response/orderResponseDto';
import { ProductResponseDto } from 'src/app/core/models/products/response/productResponseDto';
import { OrderService } from 'src/app/core/services/order.service';
import { ProductsService } from 'src/app/core/services/products.service';
import { AddOrderComponent } from '../../components/add-order/add-order.component';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit, OnDestroy {
  private modalRef?: BsModalRef;
  public orders : OrderResponseDto[] = [];
  public products: ProductResponseDto[]  = [];
  OrderStatusType= OrderStatusType;
  subscription: Subscription | null = null;
  constructor(private OrderService: OrderService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private productService: ProductsService,) { 
      this.getOrders();
      this.getProducts();
    }
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  ngOnInit(): void {
  }


  getOrders()
  {
    this.OrderService.getAllOrders().subscribe({
      next:(res)=>{
        if(res.succeed){
          this.orders = res.result;
        }
      }
    })
  }

  private getProducts(){
    this.productService.getAllProduct().subscribe({
      next:(resp)=>{
         if(resp.succeed)
          this.products = resp.result;
      }
    })
  }

  getTabOrder(status:OrderStatusType): OrderResponseDto[]{
    return this.orders?.filter(r=>r.status == status);
  }
   
  openModaOrder() {
    this.modalRef = this.modalService.show(AddOrderComponent);
    this.subscription = this.modalRef.content.onClose.subscribe({next:(value:boolean)=>{
      if(value)this.getOrders();
   }})
  }

  changeStatusEvent(){
    this.getProducts();
    this.getOrders();
  }

}
