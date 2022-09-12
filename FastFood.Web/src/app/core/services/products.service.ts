import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiListResponseDto } from '../models/apiListResponseDto';
import { ApiResponseDto } from '../models/apiResponseDto';
import { EmptyResponseDto } from '../models/emptyResponseDto';
import { ProductRequestDto } from '../models/products/request/productRequestDto';
import { ProductResponseDto } from '../models/products/response/productResponseDto';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  private baseUrl:string = "/api/Products";
  constructor(private http: HttpClient) { }

  createProduct(dto:ProductRequestDto):Observable<ApiResponseDto<ProductResponseDto>>{
    return this.http.post<ApiResponseDto<ProductResponseDto>>(this.baseUrl,dto);
  }

  getProduct(id:number):Observable<ApiResponseDto<ProductResponseDto>>{
    return this.http.get<ApiResponseDto<ProductResponseDto>>(this.baseUrl + `?id=${id}`);
  }

  deleteProduct(id:number):Observable<EmptyResponseDto>{
    return this.http.delete<EmptyResponseDto>(this.baseUrl + `?id=${id}`);
  }

  getAllProduct():Observable<ApiListResponseDto<ProductResponseDto>>{
    return this.http.get<ApiListResponseDto<ProductResponseDto>>(this.baseUrl)
  }

  updateProduct(id:number, dto: ProductRequestDto):Observable<ApiResponseDto<ProductResponseDto>>{
    return this.http.put<ApiResponseDto<ProductResponseDto>>(this.baseUrl + `?id=${id}`,dto);
  }
  
}
