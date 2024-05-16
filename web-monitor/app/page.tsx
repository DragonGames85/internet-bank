'use client';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import { useEffect, useState } from 'react';
import { faker } from '@faker-js/faker';
ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend);
import { Line } from 'react-chartjs-2';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';
import { GLOBAL_API } from './api';

export default function Home() {
    const [statusCodes, setStatusCodes] = useState<number[]>([]);
    const [responseTimes, setResponseTimes] = useState<number[]>([]);

    const [body, setBody] = useState<GLOBAL_API['/monitoring/api/all/tracing']['body']>();

    const [begin, setBegin] = useState('');
    const [end, setEnd] = useState('');

    const options = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top' as const,
            },
            title: {
                display: true,
                text: 'График стабильности',
            },
        },
    };

    const getRandomValue = () => {
        const randomStatus = faker.datatype.number({ min: 200, max: 500 });
        setStatusCodes(prev => [...prev, randomStatus]);
        setResponseTimes(prev => {
            if (prev.length == 0) {
                return [1];
            } else {
                return [...prev, prev[prev.length - 1] + 1];
            }
        });
        toast.success(`Новый запрос: ${randomStatus}`);
    };

    const data = {
        labels: responseTimes,
        datasets: [
            {
                label: 'СТАТУС КОД',
                data: statusCodes,
                borderColor: 'rgb(255, 99, 132)',
                backgroundColor: 'rgba(255, 99, 132, 0.5)',
            },
        ],
    };

    const getAllTracing = async (data: GLOBAL_API['/monitoring/api/all/tracing']['parameters']) => {
        let result = null;
        try {
            result = await axios.get<GLOBAL_API['/monitoring/api/all/tracing']['body']>(
                'url' + '/monitoring/api/all/tracing',
                {
                    params: data,
                }
            );
            setBody(result.data);
        } catch (error) {}
    };

    // getAllTracing({});

    return (
        <main className="flex min-h-screen flex-col items-center justify-between px-12 py-2">
            <div className="flex items-center justify-center">
                <label>begin</label>
                <input type="date" className="mb-2" onChange={e => setBegin(e.target.value)} />
                <label>end</label>
                <input type="date" onChange={e => setEnd(e.target.value)} />
            </div>
            <Line options={options} data={data} />
            <button
                onClick={() => getAllTracing({ begin, end })}
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            >
                Получить
            </button>
            <button
                onClick={getRandomValue}
                className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
            >
                Мокнуть запрос
            </button>
            <ToastContainer />
        </main>
    );
}
