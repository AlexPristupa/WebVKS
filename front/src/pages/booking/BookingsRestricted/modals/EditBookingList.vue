<template>
  <div class="mp-modal--booking__list" :class="listClass">
    <el-input
      clearable
      v-model="search"
      class="mp-modal--booking__list__search"
      :disabled="searchDisabled"
      :placeholder="$t('forms.placeholders.search')"
      @input="searchOptions"
    />
    <div class="mp-modal--booking__list__list">
      <mp-loading-spinner v-if="loading" />
      <el-checkbox
        v-else
        class="mp-modal--booking__list__item"
        v-for="(item, index) in list"
        :disabled="
          listClass === classes.participants && item.id === selectedOwner
        "
        :class="{ 'without-checkbox': listClass === classes.spaces }"
        :value="item.checked"
        :key="index"
        @change="setEntity(item)"
      >
        <span>
          {{ getLabel(item) }}
        </span>
        <mp-button
          v-if="listClass === classes.participants"
          :disabled="!item.checked"
          :mp-type="buttonType"
          :icon="buttonIcon"
          class="mp-button--cell mp-cell__action-button"
          size="mini"
          circle
          @click="openParticipantDialog(item)"
        />
      </el-checkbox>
    </div>
    <edit-booking-list-participant-modal
      v-if="isVisibleParticipantDialog"
      :space-id="spaceId"
      :selected-entity="selectedItem"
      :visible-edit-modal="isVisibleParticipantDialog"
      @create="createUser"
      @update="editList"
      @close="isVisibleParticipantDialog = false"
    />
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import { debounce } from '@/utils'
import CONSTANTS from '@/constants'
import {
  BookingModalListType,
  IComputedBookingList,
  IDataBookingList,
  IMethodsBookingList,
  IPropsBookingList,
  listClass,
  selectType,
} from '@/pages/booking/Bookings/modals/config/editBookingList.interface'
import { VksSelectOptionEntity } from '@/modules/ApiDataValidation/ResponseDto/Options/VksSelectOption.entity'
import MpLoadingSpinner from '@/components/MpLoadingSpinner/MpLoadingSpinner.vue'
import MpButton from '@/components/basic/MpButton/MpButton.vue'
import { MpTypeButton } from '@/components/basic/MpButton/MpButton.const'
import { ElementUIIcons } from '@/components/MpTable/cell/config/MpCellActionButton.interface'
import EditBookingListParticipantModal from '@/pages/booking/BookingsRestricted/modals/EditBookingListParticipant.modal.vue'
import { VksBookingLinkToParticipant } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkToParticipant.entity'
import { methods } from '@/api_services/httpMethods.enum'
import { VksBookingLinkBookingToVksUsersOthers } from '@/modules/ApiDataValidation/ResponseDto/Booking/VksBookingLinkBookingToVksUsersOthers.entity'
import { VksUserOtherEntity } from '@/modules/ApiDataValidation/ResponseDto/UserOthers/UserOther.entity'
import { VksUserEntity } from '@/modules/ApiDataValidation/ResponseDto/User/User.entity'

export default Vue.extend<
  IDataBookingList,
  IMethodsBookingList,
  IComputedBookingList,
  IPropsBookingList
>({
  name: 'EditBookingList',
  components: { EditBookingListParticipantModal, MpButton, MpLoadingSpinner },
  props: {
    listClass: {
      type: String as () => listClass,
      default: listClass.spaces,
    },
    selectType: {
      type: String as () => selectType,
      default: selectType.single,
    },
    options: {
      type: Array as () => Array<VksSelectOptionEntity>,
      default: () => [],
    },
    checked: {
      type: Array as () => Array<
        | VksSelectOptionEntity
        | VksBookingLinkToParticipant
        | VksBookingLinkBookingToVksUsersOthers
      >,
      default: () => [],
    },
    checkedCopy: {
      type: Array as () => Array<
        VksBookingLinkToParticipant | VksBookingLinkBookingToVksUsersOthers
      >,
      default: () => [],
    },
    spaceId: {
      type: Number,
      default: 0,
    },
    selectedOwner: {
      type: Number,
      default: 0,
    },
    loading: {
      type: Boolean,
      default: false,
    },
  },
  computed: {
    searchDisabled() {
      return !this.list.length && !this.search
    },
    checkedIdList() {
      if (this.checked?.length) {
        return this.checked.map(item => item.id)
      }
      return []
    },
  },
  data() {
    return {
      list: [],
      search: '',
      classes: {
        spaces: listClass.spaces,
        participants: listClass.participants,
      },
      isVisibleParticipantDialog: false,
      selectedItem: new VksBookingLinkToParticipant(),
      buttonType: MpTypeButton.edit,
      buttonIcon: ElementUIIcons.edit,
    }
  },
  watch: {
    selectedOwner(value) {
      if (
        this.listClass === listClass.participants &&
        this.checkedIdList.includes(value)
      ) {
        const participant: BookingModalListType | undefined = this.list.find(
          part => part.id === value,
        )
        if (participant && participant.id) {
          this.setEntity(participant)
        }
      }
    },
    checked() {
      this.setCheckboxes()
    },
    options(value) {
      this.list = JSON.parse(JSON.stringify(value))
      this.setCheckboxes()
    },
  },
  methods: {
    setCheckboxes() {
      if (this.list.length) {
        this.list = this.list.map(item => {
          item.checked = this.checkedIdList.includes(item.id)
          return item
        })
      }
    },
    searchOptions: debounce(
      function(this) {
        this.$emit('search', this.search.toLowerCase())
      },
      CONSTANTS.debounce.timeOut.slow,
      false,
    ),
    async setEntity(item) {
      let checked: Array<
        | VksSelectOptionEntity
        | VksBookingLinkToParticipant
        | VksBookingLinkBookingToVksUsersOthers
      > = []
      if (this.selectType === selectType.multiple) {
        checked = await this.toggleCheckParticipants(item)
      } else {
        checked = this.options.filter(option => option.id === item.id)
      }
      this.$emit('update', checked)
    },
    async toggleCheckParticipants(item) {
      const checked = [...this.checked]
      if (this.checkedIdList.includes(item.id)) {
        const index = checked.findIndex(option => {
          return option.id === item.id
        })
        checked.splice(index, 1)
      } else {
        const existed = this.checkedCopy?.length
          ? (this.checkedCopy?.find(
              existedUser => existedUser.id === item.id,
            ) as
              | VksBookingLinkToParticipant
              | VksBookingLinkBookingToVksUsersOthers)
          : null
        const user = existed ? existed : await this.getUser(item.id)
        const checkedUser = !(item as
          | VksBookingLinkToParticipant
          | VksBookingLinkBookingToVksUsersOthers).isFromOtherList
          ? new VksBookingLinkToParticipant(user)
          : new VksBookingLinkBookingToVksUsersOthers(user)
        checked.push(checkedUser)
      }
      return checked
    },
    getLabel(item) {
      const copyItem = item as
        | VksUserOtherEntity
        | VksBookingLinkToParticipant
        | VksBookingLinkBookingToVksUsersOthers
      let label = copyItem.name
      if (this.listClass === this.classes.participants) {
        if (copyItem.uri) {
          label = `${copyItem.uri} ${copyItem.name}`
        }
        if (copyItem.email) {
          label = `${label} (${copyItem.email})`
        }
      }
      return label.toString()
    },
    async getUser(id) {
      let res = false
      if (id.toString().includes('other')) {
        res = await this.$api.vksUserOther({
          method: methods.get,
          data: {
            id: id.toString().replace(/other/g, ''),
          },
        })
        if (res) {
          return new VksUserOtherEntity(res)
        }
      } else {
        res = await this.$api.vksUser({
          method: methods.get,
          data: {
            id: id,
          },
        })
        if (res) {
          return new VksUserEntity({ ...(res as {}), id: id })
        }
      }
    },
    openParticipantDialog(item) {
      const selectedToEdit = this.checked?.find(option => option.id === item.id)
      if (selectedToEdit) {
        this.selectedItem = selectedToEdit
        this.isVisibleParticipantDialog = true
      }
    },
    createUser(createdUser) {
      const checked = this.checked
      checked?.unshift(createdUser)
      this.$emit('create', createdUser)
      this.$emit('update', checked)
    },
    editList(editedUser) {
      const checked = this.checked
      const index = checked.findIndex(option => {
        return option.id === editedUser.id
      })
      checked.splice(index, 1)
      checked.push(editedUser)
      this.$emit('update-one', editedUser)
      this.$emit('update', checked)
    },
  },
})
</script>
