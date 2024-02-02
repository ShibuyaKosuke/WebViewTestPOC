function scenario_toolbar(scenario_header) {
    const scenario_toolbar = document.createElement('div');
    scenario_toolbar.id = 'scenario_toolbar';

    const scenario_toolbar_title = document.createElement('div');
    scenario_toolbar_title.innerText = '項目をドラッグ';
    scenario_toolbar.appendChild(scenario_toolbar_title);

    document.body.appendChild(scenario_toolbar);

    const items = scenario_header.map(header => {
        const item = document.createElement('div');
        item.innerText = header; scenario_toolbar.appendChild(item);
        return item;
    });
}
