import { OrderStatusType } from "src/app/core/enums/orderStatusType";
import { OrderProductResponseDto } from "./orderProductResponseDto";

export interface OrderResponseDto{
    id: number;
    orderNumber:string;
    description: string;
    products: OrderProductResponseDto[];
    status: OrderStatusType;
    createdAt: Date;
    updatedAt?: Date;
}
