import axios from 'axios';
import { Operation } from './types';
import { coreAppUrl, setURL } from '../config';

export class operationsApi {
    public getAll(accountId: string) {
        setURL(coreAppUrl);
        return axios.get<Operation[]>(`/Operation/account/${accountId}`).then(res => res.data);
    }
}
