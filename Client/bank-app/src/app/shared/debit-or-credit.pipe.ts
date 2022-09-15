import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'DebitOrCredit'
})
export class DebitOrCreditPipe implements PipeTransform {

  transform(isDebit: boolean ): string {
    if(isDebit){
      return 'Debit';
    }
    else{ 
      return 'Credit';
    }
  }

}
