import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-email-verifiction',
  templateUrl: './email-verifiction.component.html',
  styleUrls: ['./email-verifiction.component.scss']
})
export class EmailVerifictionComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<EmailVerifictionComponent>,
    @Inject(MAT_DIALOG_DATA) public code: number,
  ) {}

  ngOnInit(): void {
  }

}
