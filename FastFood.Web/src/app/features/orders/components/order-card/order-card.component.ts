import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { OrderStatusType } from 'src/app/core/enums/orderStatusType';
import { OrderResponseDto } from 'src/app/core/models/orders/response/orderResponseDto';
import { ProductResponseDto } from 'src/app/core/models/products/response/productResponseDto';
import { OrderService } from 'src/app/core/services/order.service';

@Component({
  selector: 'app-order-card',
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.scss']
})
export class OrderCardComponent implements OnInit {

  @Input()
  order!: OrderResponseDto;

  @Input()
  products: ProductResponseDto[] = [];

  @Output()
  changeStatus: EventEmitter<any>= new EventEmitter();

  OrderStatusType= OrderStatusType;
  constructor(public modalRef: BsModalRef,
    private toastr: ToastrService,
    private orderService: OrderService) { }

  ngOnInit(): void {
  }


  getOrderStatus(status: OrderStatusType) {
    switch (status) {
      case OrderStatusType.Pending:
        return "Pending";
      case OrderStatusType.Canceled:
        return "Canceled";
      case OrderStatusType.Completed:
        return "Completd";
      case OrderStatusType.Delivered:
        return "Delivered";
      case OrderStatusType.InProgress:
        return "In progress";
      default:
        return "Pending";     
    }
  }

  get selectedProducts():any[]{
     return this.order.products.map(s=>{
      return {product:this.products.find(r=>r.id == s.productId)!, quantity:s.quantity};
    })
  }

  changeCardStatus(status: OrderStatusType){
    this.orderService.changeStatus(this.order.id,status).subscribe(r=> {
      if(r.succeed)
      {
        this.order = r.result;
        this.toastr.success("statud change to:" + this.getOrderStatus(status))
        this.changeStatus.next(this.order);
      }
    })
  }
}
