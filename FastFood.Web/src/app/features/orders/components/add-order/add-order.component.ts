import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { EmptyResponseDto } from 'src/app/core/models/emptyResponseDto';
import { OrderRequestDto } from 'src/app/core/models/orders/request/orderRequestDto';
import { ProductResponseDto } from 'src/app/core/models/products/response/productResponseDto';
import { OrderService } from 'src/app/core/services/order.service';
import { ProductsService } from 'src/app/core/services/products.service';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.css']
})
export class AddOrderComponent implements OnInit {

  description: string = "";
  products: ProductResponseDto[] = [];

  form: FormGroup = new FormGroup({
    description: new FormControl('', [Validators.required]),
    products: new FormArray([])
  })
  public onClose: Subject<boolean> = new Subject();
  constructor(public modalRef: BsModalRef,
    private toastr: ToastrService,
    private productsService: ProductsService,
    private orderService: OrderService) {
    this.getProducts();
  }

  ngOnInit(): void {
    this.addProduct();
  }

  getProducts() {
    this.productsService.getAllProduct().subscribe({
      next: (res) => {
        if (res.succeed) {
          this.products = res.result;
        }
      }
    })
  }

  onFormSubmit(): void {
    if (this.form.invalid) {
      this.toastr.warning("some data is required")
      return;
    }

    let product: OrderRequestDto = this.form.value;
    this.orderService.createOrder(product).subscribe({
      next: (resp) => {
        this.onClose.next(true);
        this.modalRef.hide();
        this.toastr.success("Order has been created");
      },
       error:(error)=>{   
          this.toastr.error("An error has occurred");

      }});
  }


  newProduct(): FormGroup {
    return new FormGroup({
      productId: new FormControl<number | null>(null, [Validators.required]),
      quantity: new FormControl<number>(0, [Validators.required, Validators.min(1)]),
    })
  }

  addProduct() {
    let isValid = true;

    this.orderProduct.controls.forEach(control => {
      let productId = control.get('productId')?.value;
      let quantity = control.get('quantity')?.value;

      let product = this.products.find(r => r.id == productId)!;
      if (product?.stock! < quantity) {
        this.toastr.warning(`product ${product?.name} not avilable`);
        isValid = false;
        return;
      }
    });

    if (!isValid)
      return;


    this.orderProduct.push(this.newProduct());
  }

  removeproduct(i: number) {
    this.orderProduct.removeAt(i);
  }

  get orderProduct(): FormArray {
    return this.form.get("products") as FormArray
  }
}
