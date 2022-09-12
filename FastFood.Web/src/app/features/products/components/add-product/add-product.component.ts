import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { EmptyResponseDto } from 'src/app/core/models/emptyResponseDto';
import { ProductRequestDto } from 'src/app/core/models/products/request/productRequestDto';
import { ProductResponseDto } from 'src/app/core/models/products/response/productResponseDto';
import { ProductsService } from 'src/app/core/services/products.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {

  @Input() product: ProductResponseDto | null = null;

  public onClose: Subject<boolean> = new Subject();
  
  public isUpdate: boolean = false;
  public form: FormGroup = new FormGroup({
    name: new FormControl<string>('', [Validators.required]),
    description: new FormControl<string>('',[Validators.required]),
    productCode: new FormControl<string>('',[Validators.required]),
    stock: new FormControl<number>(0,[Validators.required, Validators.min(1)])
  });
  constructor(public modalRef: BsModalRef, 
    private productService: ProductsService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    if(this.product)
    {
      this.isUpdate = true;
      this.loadForm();
    }
  }

  
  onFormSubmit(): void {
    let product:ProductRequestDto = Object.assign(this.form.value);
    if(this.isUpdate){
      this.productService.updateProduct(this.product?.id!,product).subscribe({
        next:(resp)=>{
          if(resp.succeed){
            this.toastr.success("The product has been updated");
            this.onClose.next(true);
            this.modalRef.hide();
          }
        },
        error:(error)=>{
          let messae : EmptyResponseDto = error.error;
          if(messae.errorMessageCode == 103){
            this.toastr.error( "The product exist","An error has occurred",);
            return;
          }
         
            this.toastr.error("An error has occurred");

        }});
    }
    else{
      this.productService.createProduct(product).subscribe({
        next:(resp)=>{
          if(resp.succeed){
            this.toastr.success("The product has been created");
            this.onClose.next(true);
            this.modalRef.hide();
          }
        },
        error:(error)=>{
          let messae : EmptyResponseDto = error.error;
          console.log(messae)
          if(messae.errorMessageCode == 103){
            this.toastr.error( "The product exist","An error has occurred",);
            return;
          }
         
            this.toastr.error("An error has occurred");

        }
      })
    }
  }


  private loadForm(){
    this.form.patchValue({
      name: this.product?.name,
      description: this.product?.description,
      productCode: this.product?.productCode,
      stock:this.product?.stock
    })
  }



}
