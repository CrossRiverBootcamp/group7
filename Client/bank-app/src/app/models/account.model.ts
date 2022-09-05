export interface AccountModel{
  id:number;
  customerId:number;
  firstName:string;
  lastName:string;
  email:string;
  openDate:Date;
  balance:number;

  // constructor() {
  //   this.id = 0;
  //   this.customerId = 0;
  //   this.firstName = ""; 
  //   this.lastName = "";
  //   this.email = "";
  //   this.openDate = new Date();
  //   this.balance = 0;
  // }
}