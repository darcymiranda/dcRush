import React, {useEffect, useState} from 'react';
import {Table, Input, Space, Button} from 'antd';

import 'antd/dist/antd.css';

export default function PlayerRushingStats() {
    return (
        <>
            <h2>Player Rushing Stats</h2>
            <PlayerStatsTable/>
        </>
    );
}

const PlayerStatsTable = () => {

    const endpoint = "/api/playerRushingStats";

    const [loading, setLoading] = useState(true);
    const [data, setData] = useState([]);
    const [exportFilter, setExportFilter] = useState({});

    useEffect(() => {
        (async () => {
            await refreshData({paging: {page: 1, pageSize: 10}});
        })();
    }, []);

    const refreshData = async (params) => {
        setLoading(true);
        const queryString = paramsToQueryString(params);
        const response = await fetch(`${endpoint}?${queryString}`);
        const json = await response.json();

        setData(json);
        setLoading(false);
    };

    const downloadCsv = async () => {
        const queryString = paramsToQueryString(exportFilter);
        const response = await fetch(`${endpoint}?${queryString}`, {
            headers: {
                accept: "text/csv"
            }
        });

        const temp = document.createElement('a');
        temp.download = "playerRushingStatsExport.csv";
        temp.href = `data:text/csv,${await response.text()}`;
        temp.dispatchEvent(new MouseEvent('click'));
    };

    const handleSearchPlayerText = (searchValue, confirm) => {
        confirm();
        setExportFilter({searchByPlayer: searchValue, ...exportFilter});
    }

    const columns = [
        {
            title: "Player",
            dataIndex: "player",
            filterDropdown: ({setSelectedKeys, selectedKeys, confirm}) => (
                <div style={{padding: 4}}>
                    <Input
                        placeholder="Search by player name..."
                        value={selectedKeys[0]}
                        onChange={e => setSelectedKeys(e.target.value ? [e.target.value] : [])}
                        onPressEnter={() => handleSearchPlayerText(selectedKeys[0], confirm)}
                        style={{width: 200, marginBottom: 12, display: 'block'}}
                    />
                    <Space>
                        <Button
                            type="primary"
                            onClick={() => handleSearchPlayerText(selectedKeys[0], confirm)}
                            size="small"
                            style={{width: 80}}
                        >
                            Search
                        </Button>
                        <Button onClick={() => {
                            setSelectedKeys([]);
                            confirm();
                        }}
                                size="small"
                                style={{width: 80}}>
                            Reset
                        </Button>
                    </Space>
                </div>
            ),
        },
        {
            title: "Team",
            dataIndex: "team"
        },
        {
            title: "Position",
            dataIndex: "pos"
        },
        {
            title: "Attempts",
            dataIndex: "att"
        },
        {
            title: "Attempts per Game",
            dataIndex: "attG"
        },
        {
            title: "Total Yards",
            dataIndex: "yds",
            sorter: (a, b) => {
                // Do nothing, handled server-side
            },
            sortDirections: ['descend', 'ascend']
        },
        {
            title: "Average Yards",
            dataIndex: "avg"
        },
        {
            title: "Yards per Game",
            dataIndex: "ydsG"
        },
        {
            title: "Total Touchdowns",
            dataIndex: "td",
            sorter: (a, b) => {
                // Do nothing, handled server-side
            },
            sortDirections: ['descend', 'ascend']
        },
        {
            title: "Longest",
            dataIndex: "lng",
            sorter: (a, b) => {
                // Do nothing, handled server-side
            },
            sortDirections: ['descend', 'ascend']
        },
        {
            title: "First Downs",
            dataIndex: "first"
        },
        {
            title: "First Down %",
            dataIndex: "firstPercentage"
        },
        {
            title: "20+ Yards Each",
            dataIndex: "twentyPlus"
        },
        {
            title: "40+ Yards Each",
            dataIndex: "fortyPlus"
        },
        {
            title: "Fumbles",
            dataIndex: "fum"
        }
    ];

    return (
        <div>
            <Table
                rowKey="player"
                size="small"
                loading={loading}
                dataSource={data.items}
                columns={columns}
                onChange={async (pagination, filters, sorter) => {
                    const order = sorter.column ? {name: sorter.column?.dataIndex, type: sorter.order} : null;
                    const paging = {page: pagination.current, pageSize: pagination.pageSize};
                    const search = filters?.player?.find(x => true) ?? "";
                    await refreshData({order, paging, search});
                    setExportFilter({order: order, search: search});
                }}
                pagination={{
                    total: data.total
                }}
            />
            <Button
                onClick={async () => await downloadCsv()}
            >Export CSV</Button>
        </div>
    )
}

const paramsToQueryString = ({order, paging, search}) => {
    const params = {};
    if (paging) {
        params.page = paging.page;
        params.pageSize = paging.pageSize;
    }

    if (search) {
        params.searchByPlayer = search;
    }

    if (order) {
        params.order = `${order.name},${order.type}`;
    }

    return Object.keys(params)
        .map(k => encodeURIComponent(k) + '=' + encodeURIComponent(params[k]))
        .join('&');
};