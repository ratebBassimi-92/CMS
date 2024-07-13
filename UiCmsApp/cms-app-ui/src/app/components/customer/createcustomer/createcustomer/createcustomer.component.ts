import { Component,OnInit,Inject } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
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
  nameAction:string='Create New Customer'
  constructor(
    private fb:FormBuilder,
    private customerService:CustomerService,
    private router: Router,
    public dialogRef: MatDialogRef<CreatecustomerComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {}


  ngOnInit(): void
  {
    debugger;
    console.log(this.data)
    if(this.data ==null)
    {
      this.nameAction='Create New Customer'
      this.createCustomerForm=this.fb.group({
        firstName:['',Validators.required],
        lastName:['',Validators.required],
        email:['',Validators.required],
        phone:['',Validators.required],
        address:['',Validators.required]
      })
  
    }
    else
    {
      this.nameAction='Update The Customer'
      this.createCustomerForm=this.fb.group({
        firstName:[this.data.firstName,Validators.required],
        lastName:[this.data.lastName,Validators.required],
        email:[this.data.email,Validators.required],
        phone:[this.data.phone,Validators.required],
        address:[this.data.address,Validators.required]
      })
  
    }
    
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
        if(this.data ==null)
        {
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
          debugger;
          this.customerService.updateCustomer(this.customerInput,this.data.customerId)
          .subscribe({
            next:(res)=>
              {
                alert("update succeessfuly")
                this.createCustomerForm.reset();
                
              },
            error:(err)=>
              {
                alert(err)
              }
          })
        }
      }
      else{
        alert("plaese insert all the inpute which is required")
      }

  }
  onClose(): void {
    this.dialogRef.close();
}

}
