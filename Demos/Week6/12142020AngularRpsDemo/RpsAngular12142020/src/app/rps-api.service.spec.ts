import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

import { RpsApiService } from './rps-api.service';

describe('RpsApiService', () => {
  let service: RpsApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: []
    });
    service = TestBed.inject(RpsApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
