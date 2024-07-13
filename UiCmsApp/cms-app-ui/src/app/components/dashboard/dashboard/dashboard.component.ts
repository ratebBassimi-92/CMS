import { Component ,OnInit} from '@angular/core';
import {CustomerService } from '../../../services/customer/customer.service'
import {Customer} from '../../../models/customer'
import { ChartOptions, ChartType,Chart,registerables } from 'chart.js';
Chart.register(...registerables)
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{

  customer !:Customer[]
  dataSource=this.customer;
  chartData:any[]=[];
  labelData:any[]=[];
  realData:any[]=[];
  colorData:any[]=[];
  constructor( 
    private customerService:CustomerService ,
  ){
  }

  ngOnInit(): void {
    this.loadData()
  }

  loadData()
  {
    this.customerService.getAnalytic().subscribe(
      {
        next:(res)=>
          {
            debugger;
            this.chartData=res.data;
            if(this.chartData !=null)
              {
                
                this.chartData.map(o =>{
                  this.labelData.push(o.city)
                  this.realData.push(o.totalCustomers)
                })
          
                this.renderChart(this.labelData,this.realData)
              }
          },
          error:(err)=>
            {
              alert(err)
            }

       }
    )
    
  }

  renderChart(labelData:any,valueData:any)
  {
    debugger;
    const mychar= new Chart('barchart',{
      type:'bar',
      data:{
        labels:labelData,
        datasets:[
          {data:valueData}
        ]
      },
      options:{

      }
    });
  }
}
