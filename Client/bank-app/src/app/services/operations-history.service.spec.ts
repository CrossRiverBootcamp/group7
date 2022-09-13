import { TestBed } from '@angular/core/testing';

import { OperationsHistoryService } from './operations-history.service';

describe('OperationsHistoryService', () => {
  let service: OperationsHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OperationsHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
