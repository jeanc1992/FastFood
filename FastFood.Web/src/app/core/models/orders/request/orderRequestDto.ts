import { OrderProductRequestDto } from "./orderProductRequestDto";

export interface OrderRequestDto{
    description:string;
    products:OrderProductRequestDto[];
}