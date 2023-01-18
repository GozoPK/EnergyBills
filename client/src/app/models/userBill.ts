export interface UserBill {
    billNumber: string;
    month: string;
    year: number;
    ammount: number;
    ammountToReturn: number;
    status: string;
    type: string;
    dateOfCreation: Date;
}