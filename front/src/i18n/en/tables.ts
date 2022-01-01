import { TableName } from '@/modules/table_grid/TableName.const'

export default {
  [TableName.booking]: {
    id: '№',
    name: 'Name',
    description: 'Description',
    owner: 'Owner',
    spaceName: 'Room',
    schedule: 'Schedule',
    spaceUri: 'URI',
    type: 'Connection type',
    currentStatus: 'Current status',
    nextRun: 'Next run',
    counter: 'Counter',
    actions: 'Actions',
  },
  [TableName.spaces]: {
    id: '№',
    name: 'Name',
    serversGroupsName: 'Group of servers',
    uri: 'URI',
    actions: 'Actions',
  },
  [TableName.editSpaceParticipants]: {
    id: '№',
    vksUser: 'Participant login',
    callLegProfileGuid: 'Call settings profile',
    rights: 'Rights',
    actions: 'Actions',
  },
  [TableName.recordings]: {
    name: 'Name',
    spaceUri: 'URI',
    owner: 'Owner',
    dateStart: 'Start date',
    dateEnd: 'Completion date',
    duration: 'Duration',
    serversGroupsName: 'Group of servers',
    actions: 'Actions',
  },
  [TableName.exportExcel]: {
    name: 'Name',
    mainTable: 'The main table',
  },
  [TableName.userProfiles]: {
    id: '№',
    name: 'Name',
    description: 'Description',
    serversGroupsName: 'Group of servers',
    actions: 'Actions',
  },
  [TableName.editSpaceBooking]: {
    name: 'Name',
    description: 'Description',
    currentStatus: 'Current status',
    nextRun: 'Next run',
  },
  [TableName.recordingsShare]: {
    user: 'User',
    isPlay: 'Watch',
    isDownload: 'Download',
    dateRecord: 'Date of given rights',
    actions: 'Actions',
  },
  [TableName.serversGroups]: {
    name: 'Name',
    description: 'Description',
    actions: 'Actions',
  },
  [TableName.servers]: {
    name: 'Name',
    basicPath: 'Basic path',
    actions: 'Actions',
  },
}