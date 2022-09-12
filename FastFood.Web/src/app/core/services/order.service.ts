import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderStatusType } from '../enums/orderStatusType';
import { ApiListResponseDto } from '../models/apiListResponseDto';
import { ApiResponseDto } from '../models/apiResponseDto';
import { OrderRequestDto } from '../models/orders/request/orderRequestDto';
import { OrderResponseDto } from '../models/orders/response/orderResponseDto';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private baseUrl:string = "/api/Order";
  constructor(private http: HttpClient) { }

  createOrder(dto: OrderRequestDto): Observable<ApiResponseDto<OrderResponseDto>>
  {
    return this.http.post<ApiResponseDto<OrderResponseDto>>(this.baseUrl,dto);
  }

  getAllOrders(status?: OrderStatusType): Observable<ApiListResponseDto<OrderResponseDto>>{
    let request= status ? this.baseUrl + `?status=${status}` : this.baseUrl;
    return this.http.get<ApiListResponseDto<OrderResponseDto>>(request);
  }

  changeStatus(id: number, status: OrderStatusType):Observable<ApiResponseDto<OrderResponseDto>>{
    return this.http.put<ApiResponseDto<OrderResponseDto>>(this.baseUrl +  `?id=${id}`, status)
  }

}
