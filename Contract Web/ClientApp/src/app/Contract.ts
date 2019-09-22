import Stage from './Stage'

export default class Contract {
  constructor(
    public id?: number,
    public contractName?: string,
    public planCost?: number,
    public factCost?: number,
    public stages?: Stage[]) { }
}
