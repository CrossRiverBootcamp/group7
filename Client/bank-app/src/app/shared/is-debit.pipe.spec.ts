import { DebitOrCreditPipe } from "./debit-or-credit.pipe";

describe('IsDebitPipe', () => {
  it('create an instance', () => {
    const pipe = new DebitOrCreditPipe();
    expect(pipe).toBeTruthy();
  });
});
