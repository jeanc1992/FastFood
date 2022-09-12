export interface ProductResponseDto{
    id :number;
    name: string;
    description:string;
    productCode:string;
    stock:number;
    createdAt: Date;
    updatedAt?: Date;
}