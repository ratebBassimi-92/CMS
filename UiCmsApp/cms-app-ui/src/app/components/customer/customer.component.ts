import { Component,OnInit} from '@angular/core';
import {CustomerService } from '../../services/customer/customer.service'
import { FormGroup,FormBuilder,Validators} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import {Customer,CustomerInput} from '../../models/customer';
import { MatDialog } from '@angular/material/dialog';
import {CreatecustomerComponent} from '../customer/createcustomer/createcustomer/createcustomer.component'
import {jsPDF} from 'jspdf'
import autoTable from 'jspdf-autotable';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  formCustomer!:FormGroup;
  customer !:Customer[]
  customerInput !:CustomerInput;
  dataSource=this.customer;

  displayedColumns: string[] = ['customerId','firstName','lastName', 'email','phone','address','action'];
  rows:any[]=[];
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
    this.customerService.getCustomerDetial(id).subscribe({
      next:(res)=>
        {
          this.customerInput=res.data

          debugger;
          const dialogRef = this.dialogCreate.open(CreatecustomerComponent, {
            width: '40%',
            height:'600px',
            data: {customerId:res.data.customerId, firstName:res.data.firstName,lastName:res.data.lastName,email:res.data.email,
                   phone:res.data.phone,address:res.data.address
            }
          });
      
          dialogRef.afterClosed().subscribe(result => {
            this.customerService.getCustomerList().subscribe(
              {
                next:(res)=>
                  {
                    this.dataSource=res.data;
                  },
                  error:(err)=>
                    {
                      alert(err)
                    }
        
               }
            )
          });
          
        },
      error:(err)=>
        {
          alert(err)
        }
  });
}

  deleteCutomer(id:any)
  {

  this.customerService.deleteCustomer(id).subscribe({
      next:(res)=>
        {
          alert(res.message);
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
        },
      error:(err)=>
        {
          alert(err)
        }
      })
  }

  openDialogCreate(): void {

    const dialogRef = this.dialogCreate.open(CreatecustomerComponent, {
      width: '40%',
      height:'600px'
    });

    dialogRef.afterClosed().subscribe(result => {
      this.customerService.getCustomerList().subscribe(
        {
          next:(res)=>
            {
              this.dataSource=res.data;
            },
            error:(err)=>
              {
                alert(err)
              }
  
         }
      )
    });

    
  }

  generatePdf(){
    const margins={
      top:10,
      bottom:30,
      left:10,
      right:10
    }
    
  const doc= new jsPDF();

  doc.setFont('Times');
  doc.setFontSize(14)
  doc.text('',margins.left,margins.top);
   const logo = '../../../assets/Danat.png';

   // Add the logo to the PDF
   doc.addImage(logo, 'PNG', 10, 10, 50, 30); // (image, format, x, y, width, height)

    // name header column 
   const columns = ['FirstName', 'LastName', 'Email','Phone','Address'];
 
    this.dataSource.forEach((item, index) => {
    this.rows.push([item.firstName,item.lastName,item.email,item.phone,item.address])
     })

    // Add the table to the PDF
      autoTable(doc, {
         head: [columns],
         body: this.rows,
         theme: 'striped', // 'striped', 'grid' or 'plain'
         headStyles: { fillColor: [255, 0, 0] }, // Red header
         bodyStyles: { fontSize: 10 }, // Font size for body cells
         margin: { top: 50 },
         didDrawPage: (data) => {
          doc.text('Customer List Report', 80, 10);
        }
       });

    doc.autoPrint();
    doc.save('CustomerList.pdf');
    
  }

}
