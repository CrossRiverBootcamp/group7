export interface TransactionModel {
  id: number;
  fromAccountId: number;
  toAccountId: number;
  amount: number;
  date: Date;
  status: Status;
  failureReason?: string;

}

export enum Status {
  processing,
  success
}