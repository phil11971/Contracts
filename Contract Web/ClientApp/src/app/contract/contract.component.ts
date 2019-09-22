import { Component, OnInit } from '@angular/core';
import Contract from '../Contract';
import { DataService } from '../data.service';

@Component({
  selector: 'app-contract',
  templateUrl: './contract.component.html',
  providers: [DataService]
})

export class ContractComponent implements OnInit {

  contract: Contract = new Contract();   // изменяемый товар
  contracts: Contract[];
  tableMode: boolean = true;          // табличный режим

  constructor(private dataService: DataService) { }

  ngOnInit() {
    this.loadContracts();    // загрузка данных при старте компонента  
  }
  // получаем данные через сервис
  loadContracts() {
    this.dataService.getContracts()
      .subscribe((data: Contract[]) => { this.contracts = data; console.log(JSON.stringify(data)); });
  }
  // сохранение данных
  save() {
    if (this.contract.id == null) {
      this.dataService.createContract(this.contract)
        .subscribe((data: Contract) => this.contracts.push(data));
    } else {
      this.dataService.updateContract(this.contract)
        .subscribe(data => this.loadContracts());
    }
    this.cancel();
  }
  editContract(c: Contract) {
    this.contract = c;
  }
  cancel() {
    this.contract = new Contract();
    this.tableMode = true;
  }
  delete(c: Contract) {
    this.dataService.deleteContract(c.id)
      .subscribe(data => this.loadContracts());
  }
  add() {
    this.cancel();
    this.tableMode = false;
  }

}
