import { Component,OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormGroup,FormBuilder,Validators} from '@angular/forms';
import { Router } from "@angular/router";
import {CustomerInput} from '../../../../models/customer'
import {CustomerService} from '../../../../services/customer/customer.service'
@Component({
  selector: 'app-createcustomer',
  templateUrl: './createcustomer.component.html',
  styleUrls: ['./createcustomer.component.css']
})
export class CreatecustomerComponent implements OnInit{

  createCustomerForm!:FormGroup;
  customerInput!:CustomerInput;

  constructor(
    private fb:FormBuilder,
    private customerService:CustomerService,
    private router: Router,
    public dialogRef: MatDialogRef<CreatecustomerComponent>) {}

  ngOnInit(): void
  {
    
    this.createCustomerForm=this.fb.group({
      firstName:['',Validators.required],
      lastName:['',Validators.required],
      email:['',Validators.required],
      phone:['',Validators.required],
      address:['',Validators.required]
    })

  }

  saveCustomer()
  {
    if(this.createCustomerForm.valid)
      {
        this.customerInput={
          firstName:this.createCustomerForm.get('firstName')?.value,
          lastName:this.createCustomerForm.get('lastName')?.value,
          email:this.createCustomerForm.get('email')?.value,
          phone:this.createCustomerForm.get('phone')?.value,
          address:this.createCustomerForm.get('address')?.value,
          createdBy:1
        }
        this.customerService.saveCustomer(this.customerInput)
          .subscribe({
            next:(res)=>
              {
                alert(res.message)
                this.createCustomerForm.reset();
                
              },
            error:(err)=>
              {
                alert(err)
              }
          })
      }
      else{
        alert("plaese insert all the inpute which is required")
      }

  }
  onClose(): void {
    this.dialogRef.close();
}

}
