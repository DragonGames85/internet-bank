import axios from 'axios';
import { Operation } from './types';

export class operationsApi {
    public getAll(accountId: string) {
        return axios.get<Operation[]>(`/Operation/account/${accountId}`).then(res => res.data);
    }

    public post(operation: {
        name: string;
        value: number;
        receiveAccountNumber?: string;
        sendAccountNumber?: string;
        currencyName: string;
    }) {
        return () => {
            axios.post('/Operation', operation);
        };
    }
}
