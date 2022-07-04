<script setup lang="ts">
import HuddleData from '../components/huddles/HuddleData.vue';
import HuddleDatePicker from '../components/huddles/HuddleDatePicker.vue';
import { ref } from 'vue';

import moment from 'moment';

const date = ref(moment().startOf('day').toISOString());

function updateHuddleData(newDate: string) {
  date.value = newDate;
}
</script>

<template>
  <div class="flex flex-col max-h-full">
    <HuddleDatePicker @date-change="updateHuddleData" />
    <HuddleData :date="date" />
  </div>
</template>
