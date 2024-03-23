import axios from 'axios';
import { Operation } from './types';
import { coreAppUrl, setURL } from '../config';

export class operationsApi {
    public getAll(accountId: string) {
        setURL(coreAppUrl);
        return axios.get<Operation[]>(`/Operation/account/${accountId}`).then(res => res.data);
    }

    public post(operation: {
        name: string;
        value: number;
        receiveAccountNumber?: string;
        sendAccountNumber?: string;
        currencyName: string;
    }) {
        setURL(coreAppUrl);
        return () => {
            axios.post('/Operation', operation);
        };
    }
}
