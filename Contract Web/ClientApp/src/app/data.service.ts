import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Contract from './Contract';

@Injectable()
export class DataService {

  private url = "/api/contracts";

  constructor(private http: HttpClient) {
  }

  getContracts() {
    return this.http.get(this.url);
  }

  getStages()
  {
    return this.http.get("/api/stages");
  }

  createContract(contract: Contract) {
    return this.http.post(this.url, contract);
  }
  updateContract(contract: Contract) {
    return this.http.put(this.url + '/' + contract.id, contract);
  }
  deleteContract(id: number) {
    return this.http.delete(this.url + '/' + id);
  }
}
