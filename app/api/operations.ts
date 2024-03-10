import axios from 'axios';
import { Operation } from './types';

export class operationsApi {
    public getAll(accountId?: string) {
        return axios.get<Operation[]>(`/Operation/account/${accountId}`).then(res => res.data);
    }
}
