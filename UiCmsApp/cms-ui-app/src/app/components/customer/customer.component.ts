import { Component,OnInit} from '@angular/core';
import {CustomerService } from '../../services/customer/customer.service'
import { FormGroup,FormBuilder,Validators} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import {Customer} from '../../models/customer';
import { MatDialog } from '@angular/material/dialog';
import {CreatecustomerComponent} from '../customer/createcustomer/createcustomer/createcustomer.component'
@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  formCustomer!:FormGroup;
  customer !:Customer[]
  dataSource=this.customer;

  displayedColumns: string[] = ['customerId','firstName','lastName', 'email','phone','address','action'];

  constructor(
    private route: ActivatedRoute,
    private customerService:CustomerService ,
    private fb:FormBuilder,
    private dialogCreate: MatDialog
  
  )
  {
  }


  ngOnInit(): void {

    this.initiateForm();
    this.customerService.getCustomerList().subscribe(
      {
        next:(res)=>
          {
            this.dataSource=res.data;
            console.log(this.dataSource);
          },
          error:(err)=>
            {
              alert(err)
            }

       }
    )

  }

  initiateForm()
  {
    this.formCustomer=this.fb.group({
      firstName:['',Validators.required], 
      lastName:['',Validators.required],
      email:['',Validators.required],
      phone:['',Validators.required], 
      address:['',Validators.required],
      createdBy:['',Validators.required]
    })

  }

  viewDetail(id:any)
  {

  }
  editCutomer(id:any)
  {

  }

  openDialogCreate(): void {

    const dialogRef = this.dialogCreate.open(CreatecustomerComponent, {
      width: '40%',
      height:'600px',
      data: { message: 'Hello from the main component!' }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log('Dialog result:', result);
    });
  }

  generatePdf(){}
}
