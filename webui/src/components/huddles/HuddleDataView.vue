<script setup lang="ts">
import HuddleColumn from '../huddles/HuddleColumn.vue';
import HuddleColumnHeader from '../huddles/HuddleColumnHeader.vue';
import HuddleTimeColumn from '../huddles/HuddleTimeColumn.vue';
import HuddleCurrentTime from './HuddleCurrentTime.vue';
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
  <div class="overflow-auto" v-if="start">
    <div class="flex sticky top-0 z-20 w-full">
      <div
        class="w-32 border-r-4 border-b flex-shrink-0 sticky left-0 bg-slate-900"
      ></div>
      <HuddleColumnHeader
        class="bg-slate-900 flex-shrink-0"
        v-for="profile in profiles"
        :key="profile.userProfileId"
        :profile="profile"
      />
    </div>
    <div class="flex-grow-0">
      <div class="h-[3000px] flex relative">
        <HuddleTimeColumn
          class="flex-shrink-0 sticky left-0 z-10 bg-slate-900"
          :time-count="timeCount"
        />
        <HuddleColumn
          class="flex-shrink-0 z-0"
          v-for="profile in profiles"
          :key="profile.userProfileId"
          :profile="profile"
          :start="start"
          :height="3000"
          :time-count="timeCount"
        />
        <HuddleCurrentTime v-if="isToday" :start="start" :height="3000" />
      </div>
    </div>
  </div>
</template>
