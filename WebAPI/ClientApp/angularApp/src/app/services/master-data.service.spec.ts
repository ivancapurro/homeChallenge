import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { MasterDataService } from './master-data.service';
import { APP_CONFIG } from 'src/providers/config.provider';
import { AppConfig } from 'src/types/AppConfig';

describe('MasterDataService', () => {
  let service: MasterDataService;
  let httpMock: HttpTestingController;
  const mockConfig: AppConfig = {
    apiEndpoint: 'https://localhost:7148/api'
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        MasterDataService,
        { provide: APP_CONFIG, useValue: mockConfig }
      ]
    });
    service = TestBed.inject(MasterDataService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve isAdmin selection data from the server', () => {
    const mockResponse = {
      // mock response data
    };

    service.getIsAdminSelection().subscribe(data => {
      expect(data).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${mockConfig.apiEndpoint}/masterdata/getRandomMasterData`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });
});
