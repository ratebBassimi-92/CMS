import { Component,OnInit} from '@angular/core'
import { FormGroup,FormBuilder,Validators} from '@angular/forms';
import { Router } from "@angular/router";


import {FormControl} from '@angular/forms';
import {AuthLoginService} from '../../services/auth-login/auth-login.service'
import {UserInput} from '../../models/userlogin'
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit{

  loginForm!:FormGroup;
  userInput!:UserInput;


  constructor( 
    private fb:FormBuilder,
    private loginService:AuthLoginService,
    private router: Router,
    
  ){
  }

  ngOnInit(): void
  {
    this.loginForm=this.fb.group({
      userName:['',Validators.required],
      password:['',Validators.required]
    })

  }
  submiteLogin()
  {
    var s=this.loginForm.value;
    console.log(s);
    if(this.loginForm.valid)
      {
        this.userInput={
          userName:this.loginForm.get('userName')?.value,
          password:this.loginForm.get('password')?.value,
        }
        this.loginService.loginUser(this.userInput)
          .subscribe({
            next:(res)=>
              {
                if(res.success)
                {
                  alert(res.message)
                  this.loginService.saveToken(res.data.token);
                  this.router.navigate(["/Customer"]);
                  this.loginForm.reset();
                }
                
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

}
