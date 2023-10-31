import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { getMinutes } from 'ngx-bootstrap/chronos/utils/date-getters';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{
  @Output() cancelRegister = new EventEmitter();
 visible: boolean = true;
 changeType: boolean = true;
 registerForm: FormGroup = new FormGroup({});
 maxDate: Date = new Date();
 validationErrors: string[] | undefined;

  constructor(private accountService: AccountService, 
    private fb: FormBuilder, private toastr: ToastrService , private router: Router) {}

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate.setFullYear(this.maxDate.getFullYear() - 18);
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      gender: ['male'],
      username: ['', Validators.required],
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required, 
        Validators.minLength(6), Validators.maxLength(16)]]
    })
  }

  register() {
    const dob = this.getDateOnly(this.registerForm.controls['dateOfBirth'].value);
    const values = {...this.registerForm.value, dateOfBirth: dob};
    this.accountService.register(values).subscribe({
      next: () => {
        this.router.navigateByUrl('/members')
      },
      error: error => {
        this.validationErrors = error
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  viewpassword() {
    this.visible = !this.visible;
    this.changeType = !this.changeType;
  }

  private getDateOnly(dob: string | undefined) {
    if(!dob) return;
    let theDob = new Date(dob);
    return new Date(theDob.setMinutes(theDob.getMinutes()-theDob.getTimezoneOffset()))
    .toISOString().slice(0,10);
  }
}
