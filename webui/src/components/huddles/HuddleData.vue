<script setup lang="ts">
import HuddleDataGrid from './HuddleDataGrid.vue';
import HuddleDataGridProfileColumn from './HuddleDataGridProfileColumn.vue';
import HuddleDataTimeBar from './HuddleDataTimeBar.vue';
import HuddleDataTimeHeaders from './HuddleDataTimeHeaders.vue';
import { computed, ref, watchEffect } from 'vue';

import moment from 'moment';

import type {
  FetchHuddlesProfileResponse,
  FetchHuddlesResponse,
} from '@/models/responses/fetch-huddles-response';
import { useAuthenticationStore } from '@/stores/authentication';

const props = defineProps<{ date: string }>();

const authStore = useAuthenticationStore();

const offset = new Date().getTimezoneOffset();

const profiles = ref<FetchHuddlesProfileResponse[] | null>(null);
const start = ref<string | null>(null);

const timeCount = 24;

const isToday = computed(
  () => start.value && formatDate(start.value) === formatDate()
);

watchEffect(async () => {
  const response = await authStore.get('/api/huddle', {
    date: props.date,
    timezoneOffset: `${-offset}`,
  });
  const body = (await response.json()) as FetchHuddlesResponse;
  profiles.value = body.profiles;
  start.value = body.start;
});

function formatDate(date?: string): string {
  return moment(date).startOf('day').toISOString();
}
</script>

<template>
  <div class="overflow-auto">
    <div class="flex w-max relative" v-if="profiles">
      <HuddleDataGrid
        :profiles="profiles"
        :time-slices="timeCount"
        :date="date"
      />
      <HuddleDataGridProfileColumn :profiles="profiles" />
      <HuddleDataTimeHeaders :time-slices="timeCount" />
      <HuddleDataTimeBar :start="date" v-if="isToday" />
    </div>
  </div>
</template>
