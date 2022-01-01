export enum MpTypeButton {
  unknown = '',
  properties = 'properties',
  activeProperties = 'active-properties',
  columnSetting = 'column-setting',
  info = 'info',
  close = 'close',
  add = 'add',
  create = 'create',
  remove = 'remove',
  export = 'export',
  filter = 'filter',
  abort = 'abort',
  filterContains = 'filter-contains',
  save = 'save',
  edit = 'edit',
  check = 'check',
  history = 'history',
  apply = 'apply',
  submit = 'submit',
  cancel = 'cancel',
  confirm = 'confirm',
  clear = 'clear',
  userAdd = 'useradd',
  lock = 'lock',
  import = 'import',
  issueNumber = 'issue-number',
  play = 'play',
  stop = 'stop',
  download = 'download',
  link = 'link',
  collapse = 'collapse',
  login = 'login',
  rewrite = 'rewrite',
}

export enum MpStatusButton {
  normal = 'normal',
  active = 'active',
  disabled = 'disabled',
}

export enum SizeButton {
  unknown = '',
  medium = 'medium',
  small = 'small',
  mini = 'mini',
}

export enum TypeButton {
  unknown = '',
  primary = 'primary',
  success = 'success',
  warning = 'warning',
  danger = 'danger',
  info = 'info',
  text = 'text',
}

export enum NativeTypeButton {
  button = 'button',
  submit = 'submit',
  reset = 'reset',
}

export const buttonsWithoutHint = [
  MpTypeButton.properties,
  MpTypeButton.columnSetting,
  MpTypeButton.activeProperties,
  MpTypeButton.login,
]
