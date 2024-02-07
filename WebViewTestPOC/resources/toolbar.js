function scenario_toolbar(scenario_header) {
    const scenario_toolbar = document.createElement('div');
    scenario_toolbar.id = 'scenario_toolbar';

    const scenario_toolbar_title = document.createElement('div');
    scenario_toolbar_title.className = "toolbar_title";
    scenario_toolbar_title.innerText = '項目をドラッグ';
    scenario_toolbar.appendChild(scenario_toolbar_title);

    document.body.appendChild(scenario_toolbar);

    const items = scenario_header.map(header => {
        const item = document.createElement('div');
        item.className = "toolbar_item";
        item.className = 'draggable';
        item.setAttribute('draggable', 'true');
        item.innerText = header; scenario_toolbar.appendChild(item);
        return item;
    });

    scenario_toolbar.addEventListener('dragstart', (event) => {
        event.dataTransfer.setData('text/plain', event.srcElement.innerText);
    });
    
    scenario_toolbar.addEventListener('drag', (event) => {
    });

    scenario_toolbar.addEventListener('dragend', (event) => {
        console.log('dragend');
    });

    const droppable = document.querySelectorAll("select,input");
    droppable.forEach((form) => {
        form.classList.add("droppable");
        form.addEventListener('drop', (event) => {
            event.preventDefault();

            droppableItem = event.target;

            const label = event.dataTransfer.getData('text/plain');
            droppableItem.setAttribute("dropped-item", label);
            console.log(droppableItem.name);
        });
    });

    return scenario_toolbar;
}
