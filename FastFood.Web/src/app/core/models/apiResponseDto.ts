import { EmptyResponseDto } from "./emptyResponseDto";

export interface ApiResponseDto<T> extends EmptyResponseDto{
    result:T;
}