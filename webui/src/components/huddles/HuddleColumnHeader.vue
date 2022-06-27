<script setup lang="ts">
import ProfileImage from '../ProfileImage.vue';

import type { FetchHuddlesProfileResponse } from '@/models/responses/fetch-huddles-response';

defineProps<{
  profile: FetchHuddlesProfileResponse;
}>();
</script>

<template>
  <div class="w-32 border-r border-b px-4">
    <ProfileImage
      class="w-24 h-24"
      :url="profile.profileImage ?? undefined"
      :name="profile.name"
    />
    <h4 class="text-center">{{ profile.name }}</h4>
  </div>
</template>
