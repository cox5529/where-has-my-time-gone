<script setup lang="ts">
import HuddleColumn from '../huddles/HuddleColumn.vue';
import HuddleColumnHeader from '../huddles/HuddleColumnHeader.vue';
import HuddleTimeColumn from '../huddles/HuddleTimeColumn.vue';
import { ref, watchEffect } from 'vue';

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

watchEffect(async () => {
  const response = await authStore.get('/api/huddle', {
    date: props.date,
    timezoneOffset: `${-offset}`,
  });
  const body = (await response.json()) as FetchHuddlesResponse;
  profiles.value = body.profiles;
  start.value = body.start;
});
</script>

<template>
  <template v-if="start">
    <div class="flex">
      <div class="w-32 border-r-4 border-b"></div>
      <HuddleColumnHeader
        v-for="profile in profiles"
        :key="profile.userProfileId"
        :profile="profile"
      />
    </div>
    <div class="overflow-auto flex-grow-0">
      <div class="h-[3000px] flex">
        <HuddleTimeColumn :time-count="timeCount" />
        <HuddleColumn
          v-for="profile in profiles"
          :key="profile.userProfileId"
          :profile="profile"
          :start="start"
          :height="3000"
          :time-count="timeCount"
        />
      </div>
    </div>
  </template>
</template>
