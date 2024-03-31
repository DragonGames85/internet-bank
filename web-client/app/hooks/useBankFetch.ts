import { useSession } from 'next-auth/react';
import { useState } from 'react';
import useSWR from 'swr';
import { useLocalStorage } from './useLocalStorage';

export function useBankFetch<T>(url: string, func: any, mockedData?: any) {
    const { data: session } = useSession();
    const [token, _] = useLocalStorage('token', '');

    const { data, error, isLoading, isValidating, mutate } = useSWR(session || token ? url : null, func);

    const [mocked, mock] = useState(false);

    const result: T = mocked ? mockedData : data;

    return { result, mock, error, isLoading, isValidating, mutate };
}
