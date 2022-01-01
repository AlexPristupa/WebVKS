import Vue from 'vue'
import {
  GridApi,
  ColumnApi,
  GridOptions,
  ModelUpdatedEvent,
  RowClickedEvent,
  SortChangedEvent,
  ColumnResizedEvent,
  ColumnVisibleEvent,
  DragStoppedEvent,
} from '@ag-grid-community/all-modules'

import * as tableGrid from '@/modules/table_grid'
import { TableName } from '@/modules/table_grid/TableName.const'
import { RowNode } from '@ag-grid-community/core/dist/cjs/entities/rowNode'
// import { structureTables } from '@/modules/TreeStructure/TreeStructure.const'

export type Context<Vue, K extends {}> = Vue & { selectedRowId: K }
export type mpTableContext = {
  selectedRowId: string
}

export interface IMpTableOptionsData {
  messages: IMessagesTableOptions
  context: Context<Vue, any>
  tableName: TableName
}

export interface IMessagesTableOptions {
  noData: string
}

export interface IParams {
  node?: any
  api: GridApi
  columnApi: ColumnApi
  type: string
}

const overlayNoRowsTemplate = (data: IMpTableOptionsData) => {
  return `<div class="dataTables_empty" style="position:absolute; top:100px;">${data.messages.noData}</div>`
}

const overlayLoadingTemplate =
  '<div class="preloderTableGrid" style="border: 2px; solid red;"><div class="el-loading-mask" style=""><div class="el-loading-spinner"><svg viewBox="25 25 50 50" class="circular"><circle cx="50" cy="50" r="20" fill="none" class="path"></circle></svg></div></div></div>'

export const mpTableOptions = (data: IMpTableOptionsData): GridOptions => {
  const self = data.context

  const onRowSelected = (params: IParams): void => {
    if (params.node.selected) {
      self.selectedRowId = params.node.data.id
      self.$emit('onRowSelected', params)
    }
  }

  const getRowClass = (params: IParams): string => {
    if (params.node.data.isFound) {
      return 'node__is-found'
    }
    if (params.node.data.isTitleGroup) {
      return 'node__is-title-group'
    }
    return ''
  }

  const onSortChanged = (params: SortChangedEvent): void => {
    self.$emit('onSortChanged', params)
  }

  const onRowClicked = (params: RowClickedEvent): void => {
    self.selectedRowId = params.node.data.id
    self.$emit('onRowClicked', params)
  }

  const onDragStopped = (params: DragStoppedEvent): void => {
    tableGrid.draged(
      {
        cols: params.columnApi.getAllGridColumns(),
      },
      data.tableName,
    )
  }

  const onColumnResized = (params: ColumnResizedEvent): void => {
    const columnSettings = {}
    params.columns?.forEach(column => {
      columnSettings[column.getColId()] = Math.round(column.getActualWidth())
    })

    if (data.tableName) {
      if (params.source === 'uiColumnDragged') {
        tableGrid.resized(data.tableName, columnSettings)
      }
    }
    params.api.resetRowHeights()
  }

  const onColumnVisible = (params: ColumnVisibleEvent): void => {
    tableGrid.visible(
      {
        cols: params.columnApi.getAllGridColumns(),
      },
      data.tableName,
    )
  }
  const onModelUpdated = (params: ModelUpdatedEvent): void => {
    params.api.refreshCells()
    const node = params.api.getRowNode('0')
    if (node) {
      node.setSelected(true)
    }
    const nodesArr: Array<RowNode> = []
    let foundNode: RowNode | undefined
    params.api.forEachNode(node => {
      nodesArr.push(node)
    })
    /**
     * закомментировано до появления таблиц со структурами
     */
    // if (structureTables.includes(data.tableName)) {
    // находим первый найденный ряд, устанавлеваем его как выделенный
    // foundNode = nodesArr.find(
    //   node => node.data.isFound || node.data.isInitialSelected,
    // )
    // } else if (self.selectedRowId) {}
    if (self.selectedRowId) {
      if (!foundNode) {
        foundNode = nodesArr.find(node => node.data.id === self.selectedRowId)
      }
    }
    if (foundNode) {
      foundNode.setSelected(true)
    }
    /**
     * Берем индекс выбранного элемента и убеждаемся,
     * виден ли элемент пользователю.
     */
    if (params.api.getSelectedNodes()[0]) {
      params.api.ensureIndexVisible(
        params.api.getSelectedNodes()[0].rowIndex,
        'middle',
      )
    }
    self.$emit('onModelUpdated', params)
  }

  return {
    overlayNoRowsTemplate: overlayNoRowsTemplate(data),
    overlayLoadingTemplate: overlayLoadingTemplate,
    suppressDragLeaveHidesColumns: true,
    enableCellTextSelection: true,
    suppressPropertyNamesCheck: true,
    onRowSelected,
    onSortChanged,
    onRowClicked,
    onColumnResized,
    onColumnVisible,
    onDragStopped,
    onModelUpdated,
    getRowClass,
  }
}
