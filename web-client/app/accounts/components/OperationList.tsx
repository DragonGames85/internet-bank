import { api } from '@/app/api';
import { Operation } from '@/app/api/types';
import { useBankFetch } from '@/app/hooks/useBankFetch';
import { FC } from 'react';
import { FaKey } from 'react-icons/fa';

const OperationList: FC<{ accId: string }> = ({ accId }) => {
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

    const { result: operations, mock } = useBankFetch<Operation[]>(
        `/api/operations/${accId}`,
        () => api.operations.getAll(accId),
        mockedOperations
    );

    return (
        <>
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
