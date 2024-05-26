import { api } from '@/app/api';
import { Operation } from '@/app/api/types';
import { coreWebsocketAppUrl } from '@/app/config';
import { useBankFetch } from '@/app/hooks/useBankFetch';
import { FC, useEffect } from 'react';
import { FaKey } from 'react-icons/fa';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const OperationList: FC<{ accId: string }> = ({ accId }) => {
    // const user = JSON.parse(localStorage.getItem('user') ?? '');

    //const { mutate } = useSWRConfig();

    const mockedOperations: Operation[] = [
        {
            id: '1',
            currency: { id: '1', name: 'USD', symbol: 'USD' },
            value: 1000,
        },
        {
            id: '2',
            currency: { id: '1', name: 'USD', symbol: 'USD' },
            value: -500,
        },
    ];

    const {
        result: operations,
        mock,
        mutate,
    } = useBankFetch<Operation[]>(`/api/operations/${accId}`, () => api.operations.getAll(accId), mockedOperations);

    // const [operations, setOperations] = useState([]);

    useEffect(() => {
        const userId = JSON.parse(localStorage.getItem('user') ?? '').userId;

        const url = coreWebsocketAppUrl;

        const ws = new WebSocket(`${url}/operationHub?userId=${userId}`);

        ws.onopen = () => {
            console.log('WebSocket Connection Established');

            const message = JSON.stringify({ protocol: 'json', version: 1 });
            ws.send(message + '\u001E');
        };

        ws.onmessage = event => {
            console.log(`We got ${event.data}`);

            if (JSON.stringify(event.data).includes('ReceiveOperationsUpdate')) {
                toast('Новая операция!');
                mutate();
            }
        };

        ws.onclose = e => {
            console.log(e);
            console.log('WebSocket Connection Closed');
        };

        ws.onerror = error => {
            console.log('WebSocket Error: ', error);
        };

        return () => {
            ws.close();
        };
    }, []);

    return (
        <>
            <ToastContainer />
            <div className="flex-center mt-16 mb-6 gap-5">
                <button
                    onClick={() => mock(prev => !prev)}
                    className="text-3xl border-2 rounded-2xl flex gap-2 p-1 items-center bg-bgColor2 dark:bg-bgColor3"
                >
                    Мок
                    <FaKey color={'fuchsia'} />
                </button>
                <h2 className="text-center text-4xl">История операции:</h2>
            </div>
            {operations?.map(operation => (
                <div className="flex flex-col sm:flex-row justify-between h-full bg-slate-600 py-4 mt-4 w-full 2xl:w-[60%] self-center text-xl overflow-hidden text-ellipsis items-center relative px-6 border-[1px] rounded-3xl">
                    <p>
                        Сумма: {operation.value} {operation.currency.symbol}
                    </p>
                    <p
                        className={`h-full ${
                            operation.name == 'Пополнение' ? 'bg-green-400' : 'bg-red-400'
                        } sm:absolute right-0 w-full sm:w-[40%] md:w-[35%] lg:w-[20%] flex items-center justify-center font-bold`}
                    >
                        {operation.name == 'Пополнение' ? 'Пополнение' : 'СНЯТИЕ'}
                    </p>
                </div>
            ))}
        </>
    );
};

export default OperationList;
