import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  //@Input() usersFromHomeComponent: any; // parent to child
  @Output() cancelRegister = new EventEmitter(); // child to parent
  model: any = {};


  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  register(){
     this.accountService.register(this.model).subscribe(response =>{
       console.log(response);
       this.cancel();
     } )
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
