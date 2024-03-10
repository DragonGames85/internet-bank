export interface Account {
    id: string;
    number: string;
    balance: number;
    type: number;
    user: IdName;
    currency: Currency;
    createdDate?: string;
    closedDate?: string;
}

export interface IdName {
    id: string;
    name: string;
}

export interface Credit {
    id: string;
    name: string;
    percent: number;
    maxCreditSum: number;
    minCreditSum: number;
    minRepaymentPeriod: number;
    maxRepaymentPeriod: number;
    paymentType: number;
    pennyPercent: number;
    rateType: number;
}

export interface Operation {
    id: string;
    value: number;
    name?: string;
    createdDate?: string;
    receiveAccount: Omit<Account, 'balance'>;
    sendAccount: Omit<Account, 'balance'>;
    currency: Currency;
}

export interface User {
    id: string;
    name: string;
    isBanned: boolean;
    role: 'COOP' | 'Customer'; // сотрудник или клиент
    accounts?: string[];
    credits?: string[];
}

export interface Currency {
    id: string;
    name: string;
    symbol: string;
}
