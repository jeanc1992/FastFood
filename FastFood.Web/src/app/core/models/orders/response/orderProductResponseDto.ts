export interface OrderProductResponseDto{
    id:number;
    productId: number;
    quantity: number;
    createdAt: Date;
    updatedAt?: Date;
}