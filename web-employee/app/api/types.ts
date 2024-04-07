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
    tariffName?: string;
}

export interface Operation {
    id: string;
    value: number;
    name?: string;
    createdDate?: string;
    receiveAccount?: Omit<Account, 'balance'>;
    sendAccount?: Omit<Account, 'balance'>;
    currency: Currency;
}

export interface User {
    id: string;
    name: string;
    isBanned: boolean;
    role: 'Employee' | 'Customer'; // сотрудник или клиент
}

export interface Currency {
    id: string;
    name: string;
    symbol: CurrencySymbol;
}

export type CurrencySymbol =
    | 'RUB'
    | 'AUD'
    | 'AZN'
    | 'GBP'
    | 'AMD'
    | 'BYN'
    | 'BGN'
    | 'BRL'
    | 'HUF'
    | 'VND'
    | 'HKD'
    | 'GEL'
    | 'DKK'
    | 'AED'
    | 'USD'
    | 'EUR'
    | 'EGP'
    | 'INR'
    | 'IDR'
    | 'KZT'
    | 'CAD'
    | 'QAR'
    | 'KGS'
    | 'CNY'
    | 'MDL'
    | 'NZD'
    | 'NOK'
    | 'PLN'
    | 'RON'
    | 'XDR'
    | 'SGD'
    | 'TJS'
    | 'THB'
    | 'TRY'
    | 'TMT'
    | 'UZS'
    | 'UAH'
    | 'CZK'
    | 'SEK'
    | 'CHF'
    | 'RSD'
    | 'ZAR'
    | 'KRW'
    | 'JPY';
